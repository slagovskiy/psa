<?php
include("config.inc");
include("psa.zip.php");

$timestamp = getdate();
$filename = $timestamp[0] . '.korder.xml';
$fp = fopen('/tmp/' . $filename, 'x');


$dbh = ibase_connect(DB, DBUSER, DBPASSWD ,"win1251");
$dbhbody = ibase_connect(DB, DBUSER, DBPASSWD ,"win1251");

$s .= "<?xml version=\"1.0\" encoding=\"windows-1251\"?>\n<KORDER>\n";

$query = <<<EOD
			SELECT 
				KIOSK_ORDERS.KID AS NUMBER,
				KIOSK_ORDERS.SESSIONID AS KIOSKID,
				KIOSKS.ADDRESS,
				KIOSK_ORDERS.STAMP,
				KIOSK_ORDERS.CUSTOMER,
				KIOSK_ORDERS.PHONE,
				AUTH.USERNAME AS TOPRINT_AUTHID,
				KIOSK_ORDERS.TOPRINT_AUTHDATE,
				AUTH1.USERNAME AS PRINTED_AUTHID,
				KIOSK_ORDERS.PRINTED_AUTHDATE,
				KIOSK_ORDERS.SHIP_AUTHID,
				KIOSK_ORDERS.SHIP_AUTHDATE,
				KIOSK_ORDERS.STATUS,
				KIOSK_ORDERS.CARDID,
				KIOSK_ORDERS.PRINTER_ADDRESS
			FROM
				KIOSK_ORDERS
				INNER JOIN KIOSKS ON (KIOSK_ORDERS.SESSIONID = KIOSKS.ID)
				LEFT OUTER JOIN AUTH ON (KIOSK_ORDERS.TOPRINT_AUTHID = AUTH.ID)
				LEFT OUTER JOIN AUTH AUTH1 ON (KIOSK_ORDERS.PRINTED_AUTHID = AUTH1.ID)
			ORDER BY 
				KIOSK_ORDERS.STAMP DESC
EOD;
$h = ibase_query($dbh, $query);
$rr = 0;
while (($row = ibase_fetch_object($h)) && ($rr < 1000))
{
	$s .= "\t<ORDER>\n";
	$op = $row->PRINTED_AUTHID;
	if($op == "")
		$op == "no_data";
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
			<PRINTED_AUTHID>$op</PRINTED_AUTHID>
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
					  P1.ACTION_NAME AS ACTION_NAME,
					  PH.HEADER AS ACTION_HEADER,
					  P1.PRICE AS ACTION_PRICE,
					  D.SUBACTION_ID,
					  P.ACTION_NAME AS SUBACTION_NAME,
					  P.PRICE AS SUBACTION_PRICE,
					  SUM(D.QTY) AS QTY,
					  D.PRICE,
					  D.SID,
					  D.KID
				FROM
					KIOSK_ORDER_DETAIL D
					LEFT OUTER JOIN PRICE_LISTS P ON (D.SUBACTION_ID = P.ID)
					INNER JOIN PRICE_LISTS P1 ON (D.ACTION_ID = P1.ID)
					INNER JOIN PRICEHEADER PH ON (P1.HID = PH.ID)
				WHERE
					KID = $row->KIOSKID AND 
					SID = $row->NUMBER AND 
					P1.GID = (SELECT KIOSK_PRICE.PRICE_GROUP FROM KIOSK_PRICE WHERE KIOSK_PRICE.KIOSK_ID = $row->KIOSKID) AND 
					P.GID = (SELECT KIOSK_PRICE.PRICE_GROUP FROM KIOSK_PRICE WHERE KIOSK_PRICE.KIOSK_ID = $row->KIOSKID)
				GROUP BY
					D.ACTION_ID,
					P1.ACTION_NAME,
					PH.HEADER,
					P1.PRICE,
					D.SUBACTION_ID,
					P.ACTION_NAME,
					P.PRICE,
					D.PRICE,
					D.SID,
					D.KID				
EOD;
	$hbody = ibase_query($dbhbody, $querybody);

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
	$rr++;
};
ibase_free_result($h);



$s .= "</KORDER>\n";
fwrite($fp, $s);

ibase_close($dbh);
ibase_close($dbhbody);

fclose($fp);

$zipTest = new zipfile();
$zipTest->add_file('/tmp/' . $filename, $filename);

Header("Content-type: application/octet-stream");
Header ("Content-disposition: attachment; filename=korder.xml.zip");
echo $zipTest->file();

unlink('/tmp/' . $filename);
?>
