<?php
include("config.inc");
include("psa.zip.php");

$timestamp = getdate();
$filename = $timestamp[0] . '.ioorder.xml';
$fp = fopen('/tmp/' . $filename, 'x');

$dbh = ibase_connect(DB, DBUSER, DBPASSWD ,"win1251");

$s .= "<?xml version=\"1.0\" encoding=\"windows-1251\"?>\n<IOORDER>\n";

$query = <<<EOD
			SELECT 
				OPT_ORDERS.ID AS NUMBER,
				'' AS KIOSKID,
				FIRMS.ADDRESS AS ADDRESS,
				OPT_ORDERS.RECDATE AS STAMP,
				FIRMS.FIRM AS CUSTOMER,
				FIRMS.PHONES,
				AUTH.USERNAME AS TOPRINT_AUTHID,
				OPT_ORDERS.TOPRINT_AUTHDATE,
				AUTH1.USERNAME AS PRINTED_AUTHID,
				OPT_ORDERS.PRINTED_AUTHDATE,
				OPT_ORDERS.SHIP_AUTHID,
				OPT_ORDERS.SHIP_AUTHDATE,
				OPT_ORDERS.STATUS,
				'' AS CARDID,
				OPT_ORDERS.SHIPTO AS PRINTER_ADDRESS
			FROM
				OPT_ORDERS
				LEFT OUTER JOIN AUTH ON (OPT_ORDERS.TOPRINT_AUTHID = AUTH.ID)
				LEFT OUTER JOIN AUTH AUTH1 ON (OPT_ORDERS.PRINTED_AUTHID = AUTH1.ID),
				FIRMS
			WHERE
				(OPT_ORDERS.USERID = FIRMS.ID)
EOD;

$h = ibase_query($dbh, $query);
while ($row = ibase_fetch_object($h))
{
	$s .= "\t<ORDER>\n";
	$s .= <<<EOD
		<HEADER>
			<NUMBER>$row->NUMBER</NUMBER>
			<KIOSKID>$row->KIOSKID</KIOSKID>
			<ADDRESS>$row->ADDRESS</ADDRESS>
			<STAMP>$row->STAMP</STAMP>
			<CUSTOMER>$row->CUSTOMER</CUSTOMER>
			<PHONE>$row->PHONE</PHONE>
			<TOPRINT_AUTHID>$row->TOPRINT_AUTHID</TOPRINT_AUTHID>
			<TOPRINT_AUTHDATE>$row->TOPRINT_AUTHDATE</TOPRINT_AUTHDATE>
			<PRINTED_AUTHID>$row->PRINTED_AUTHID</PRINTED_AUTHID>
			<PRINTED_AUTHDATE>$row->PRINTED_AUTHDATE</PRINTED_AUTHDATE>
			<SHIP_AUTHID>$row->SHIP_AUTHID</SHIP_AUTHID>
			<SHIP_AUTHDATE>$row->SHIP_AUTHDATE</SHIP_AUTHDATE>
			<STATUS>$row->STATUS</STATUS>
			<CARDID>$row->CARDID</CARDID>
			<PRINTER_ADDRESS>$row->PRINTER_ADDRESS</PRINTER_ADDRESS>
		</HEADER>

EOD;

	$querybody = <<<EOD
				SELECT
					D.ACTION_ID,
					P1.ACTION_NAME,
					PH1.HEADER AS ACTION_HEADER,
					P1.PRICE AS ACTION_PRICE,
					D.SUBACTION_ID,
					P2.ACTION_NAME AS SUBACTION_NAME,
					P2.PRICE AS SUBACTION_PRICE,
					SUM(D.QTY) AS QTY,
					D.PRICE,
					D.ORDERID AS SID,
					'0' AS KID
				FROM
					PRICE P1
					LEFT OUTER JOIN PRICEHEADER PH1 ON (P1.HID = PH1.ID)
					RIGHT OUTER JOIN OPT_ORDERS_DETAIL D ON (P1.ID = D.ACTION_ID)
					LEFT OUTER JOIN PRICE P2 ON (D.SUBACTION_ID = P2.ID)
				WHERE
					(D.ORDERID = $row->NUMBER)
				GROUP BY
					D.ACTION_ID,
					P1.ACTION_NAME,
					PH1.HEADER,
					P1.PRICE,
					D.SUBACTION_ID,
					P2.ACTION_NAME,
					P2.PRICE,
					D.PRICE,
					D.ORDERID
EOD;


	$hbody = ibase_query($dbh, $querybody);

	$s .= "\t\t<BODY>\n";
	while ($rowbody = ibase_fetch_object($hbody))
	{
		$s .= <<<EOD
				<ROW>
					<ACTION_ID>$rowbody->ACTION_ID</ACTION_ID>
					<ACTION_NAME>$rowbody->ACTION_NAME</ACTION_NAME>
					<ACTION_HEADER>$rowbody->ACTION_HEADER</ACTION_HEADER>
					<ACTION_PRICE>$rowbody->ACTION_PRICE</ACTION_PRICE>
					<SUBACTION_ID>$rowbody->SUBACTION_ID</SUBACTION_ID>
					<SUBACTION_NAME>$rowbody->SUBACTION_NAME</SUBACTION_NAME>
					<SUBACTION_PRICE>$rowbody->SUBACTION_PRICE</SUBACTION_PRICE>
					<QTY>$rowbody->QTY</QTY>
					<PRICE>$rowbody->PRICE</PRICE>
					<SID>$rowbody->SID</SID>
					<KID>$rowbody->KID</KID>
				</ROW>

EOD;
	};
	ibase_free_result($hbody);
	$s .= "\t\t</BODY>\n";
	$s .= "\t</ORDER>\n";
};
ibase_free_result($h);


$s .= "</IOORDER>\n";
fwrite($fp, $s);

ibase_close($dbh);

fclose($fp);

$zipTest = new zipfile();
$zipTest->add_file('/tmp/' . $filename, $filename);

Header("Content-type: application/octet-stream");
Header ("Content-disposition: attachment; filename=ioorder.xml.zip");
echo $zipTest->file();

unlink('/tmp/' . $filename);
?>
