<?php
include("config.inc");
$dbh = ibase_connect(DB, DBUSER, DBPASSWD ,"win1251");
$dbhbody = ibase_connect(DB, DBUSER, DBPASSWD ,"win1251");

$s .= "<?xml version=\"1.0\" encoding=\"windows-1251\"?>\n<IRORDER>\n";

$query = <<<EOD
			SELECT 
				ORDERS.ID AS NUMBER,
				'' AS KIOSKID,
				'' AS ADDRESS,
				ORDERS.RECDATE AS STAMP,
				USERINFO.FNAME || ' ' || USERINFO.LNAME || ' ' || USERINFO.MNAME AS CUSTOMER,
				USERINFO.PHONE,
				AUTH.USERNAME AS TOPRINT_AUTHID,
				ORDERS.TOPRINT_AUTHDATE,
				AUTH1.USERNAME AS PRINTED_AUTHID,
				ORDERS.PRINTED_AUTHDATE,
				ORDERS.SHIP_AUTHID,
				ORDERS.SHIP_AUTHDATE,
				ORDERS.STATUS,
				'' AS CARDID,
				ORDERS.SHIPTO AS PRINTER_ADDRESS
			FROM
				ORDERS
				LEFT OUTER JOIN AUTH ON (ORDERS.TOPRINT_AUTHID = AUTH.ID)
				LEFT OUTER JOIN AUTH AUTH1 ON (ORDERS.PRINTED_AUTHID = AUTH1.ID),
				USERINFO
			WHERE
				(ORDERS.USERID = USERINFO.ID)
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
					RIGHT OUTER JOIN ORDERS_DETAIL D ON (P1.ID = D.ACTION_ID)
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
};
ibase_free_result($h);

$s .= "</IRORDER>\n";
echo $s;

ibase_close($dbh);
ibase_close($dbhbody);

?>