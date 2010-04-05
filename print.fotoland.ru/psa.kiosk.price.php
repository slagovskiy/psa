<?php
include("config.inc");
$dbh = ibase_connect(DB, DBUSER, DBPASSWD ,"win1251");

if($_GET["action"] != "")
{
	$s = "<h1>" . $_GET["action"] . "</h1>";
	switch($_GET["action"])
	{
		case "edit":
		{
			$query = <<<EOD
				SELECT 
					KIOSK_PRICE.KIOSK_ID,
					KIOSK_PRICE.PRICE_GROUP
				FROM
					KIOSK_PRICE
				WHERE
EOD;
			$query .= " KIOSK_PRICE.KIOSK_ID = " . $_GET["K"];
			$h = ibase_query($dbh, $query);
			$row = ibase_fetch_object($h);
			$K = $row->KIOSK_ID;
			$P = $row->PRICE_GROUP;
			$s .= "<FORM ACTION=\"psa.kiosk.price.php\" METHOD=\"GET\">";
			$s .= "<TABLE><TR>";
			$s .= "<TD>КИОСК</TD>";
			$s .= "<TD><SELECT NAME=\"K\">";
			$query = <<<EOD
				SELECT 
					KIOSKS.ID,
					KIOSK_GROUP.GROUP_NAME || ' - ' || KIOSKS.ADDRESS AS NAME
				FROM
					KIOSK_GROUP
					INNER JOIN KIOSKS ON (KIOSK_GROUP.ID = KIOSKS.GROUPID)
				ORDER BY
					KIOSKS.GROUPID
EOD;
			$h = ibase_query($dbh, $query);
			while ($row = ibase_fetch_object($h))
			{
				$s .= "<OPTION VALUE=\"$row->ID\"";
				if ($K == $row->ID)
					$s .= " SELECTED";
				$s .= ">$row->NAME</OPTION>";
			}
			$s .= "</SELECT></TD>";
			$s .= "</TR><TR>";
			$s .= "<TD>ПРАЙС</TD>";
			$s .= "<TD><SELECT NAME=\"P\">";
			$query = <<<EOD
				SELECT 
					PRICEGROUPS.ID,
					PRICEGROUPS.COMMENT
				FROM
					PRICEGROUPS
EOD;
			$h = ibase_query($dbh, $query);
			while ($row = ibase_fetch_object($h))
			{
				$s .= "<OPTION VALUE=\"$row->ID\"";
				if ($P == $row->ID)
					$s .= " SELECTED";
				$s .= ">$row->COMMENT</OPTION>";
			}
			$s .= "</SELECT></TD>";
			$s .= "</TR></TABLE><INPUT TYPE=\"SUBMIT\" VALUE=\"СОХРАНИТЬ\"><BUTTON onclik=\"javascript:location.href=psa.kiosk.price.php;\">НАЗАД</BUTTON><INPUT TYPE=\"HIDDEN\" NAME=\"action\" VALUE=\"save\"></FORM>";
			break;
		}
		case "delete":
		{
			$s .= "<FORM ACTION=\"psa.kiosk.price.php\" METHOD=\"GET\">";
			$s .= "<INPUT TYPE=\"SUBMIT\" VALUE=\"УДАЛИТЬ!\"><BUTTON onclik=\"javascript:location.href=psa.kiosk.price.php;\">НАЗАД</BUTTON><INPUT TYPE=\"HIDDEN\" NAME=\"action\" VALUE=\"deleteyes\"><INPUT TYPE=\"HIDDEN\" NAME=\"K\" VALUE=\"" . $_GET["K"] ."\">";
			$s .= "</FORM>";
			break;
		}
		case "new":
		{
			$s .= "<FORM ACTION=\"psa.kiosk.price.php\" METHOD=\"GET\">";
			$s .= "<TABLE><TR>";
			$s .= "<TD>КИОСК</TD>";
			$s .= "<TD><SELECT NAME=\"K\">";
			$query = <<<EOD
				SELECT 
					KIOSKS.ID,
					KIOSK_GROUP.GROUP_NAME || ' - ' || KIOSKS.ADDRESS AS NAME
				FROM
					KIOSK_GROUP
					INNER JOIN KIOSKS ON (KIOSK_GROUP.ID = KIOSKS.GROUPID)
				WHERE
					KIOSKS.ID NOT IN (SELECT KIOSK_PRICE.KIOSK_ID FROM KIOSK_PRICE)
				ORDER BY
					KIOSKS.GROUPID
EOD;
			$h = ibase_query($dbh, $query);
			while ($row = ibase_fetch_object($h))
			{
				$s .= "<OPTION VALUE=\"$row->ID\">$row->ID: $row->NAME</OPTION>";
			}
			$s .= "</SELECT></TD>";
			$s .= "</TR><TR>";
			$s .= "<TD>ПРАЙС</TD>";
			$s .= "<TD><SELECT NAME=\"P\">";
			$query = <<<EOD
				SELECT 
					PRICEGROUPS.ID,
					PRICEGROUPS.COMMENT
				FROM
					PRICEGROUPS
EOD;
			$h = ibase_query($dbh, $query);
			while ($row = ibase_fetch_object($h))
			{
				$s .= "<OPTION VALUE=\"$row->ID\">$row->COMMENT</OPTION>";
			}
			$s .= "</SELECT></TD>";
			$s .= "</TR></TABLE><INPUT TYPE=\"SUBMIT\" VALUE=\"СОХРАНИТЬ\"><BUTTON onclik=\"javascript:location.href=psa.kiosk.price.php;\">НАЗАД</BUTTON><INPUT TYPE=\"HIDDEN\" NAME=\"action\" VALUE=\"save_new\"></FORM>";
			break;
		}
		case "save_new":
		{
			$K = $_GET["K"];
			$P = $_GET["P"];
			$ok=ibase_query($dbh, 'insert into KIOSK_PRICE(KIOSK_ID,PRICE_GROUP) values(?,?)',$K,$P);
			header('Location: psa.kiosk.price.php');
			break;
		}
		case "deleteyes":
		{
			$K = $_GET["K"];
			$sth=ibase_query("delete from KIOSK_PRICE where KIOSK_ID=?\n",$K);
			header('Location: psa.kiosk.price.php');
			break;
		}
		case "save":
		{
			$K = $_GET["K"];
			$P = $_GET["P"];
			$sth=ibase_query("update KIOSK_PRICE set KIOSK_ID=?, PRICE_GROUP=? where KIOSK_ID=? ;\n",$K,$P,$K);
			header('Location: psa.kiosk.price.php');
			break;
		}
	}

	echo $s;
}
else
{
$query = <<<EOD
	SELECT 
		KIOSKS.ID,
		KIOSK_GROUP.GROUP_NAME || ' - ' || KIOSKS.ADDRESS AS KIOSK,
		PRICEGROUPS.COMMENT
	FROM
		PRICEGROUPS
		INNER JOIN KIOSK_PRICE ON (PRICEGROUPS.ID = KIOSK_PRICE.PRICE_GROUP)
		INNER JOIN KIOSKS ON (KIOSK_PRICE.KIOSK_ID = KIOSKS.ID)
		INNER JOIN KIOSK_GROUP ON (KIOSKS.GROUPID = KIOSK_GROUP.ID)
EOD;

$h = ibase_query($dbh, $query);
$s = "<h1>Справочник соотвествия киоск-прайс.</h1><TABLE><TR><TD>#</TD><TD>Киоск</TD><TD>Прайс</TD><TD> </TD><TD> </TD></TD>";
while ($row = ibase_fetch_object($h))
{
	$s .= <<<EOD
	<TR>
		<TD style="border-left: 1px solid black; border-top: 1px solid black; border-bottom: 1px solid black;">$row->ID</TD>
		<TD style="border-left: 1px solid black; border-top: 1px solid black; border-bottom: 1px solid black;">$row->KIOSK</TD>
		<TD style="border-left: 1px solid black; border-top: 1px solid black; border-bottom: 1px solid black;">$row->COMMENT</TD>
		<TD style="border-left: 1px solid black; border-top: 1px solid black; border-bottom: 1px solid black;"><a href="psa.kiosk.price.php?action=edit&K=$row->ID">edit</a></TD>
		<TD style="border-left: 1px solid black; border-right: 1px solid black; border-top: 1px solid black; border-bottom: 1px solid black;"><a href="psa.kiosk.price.php?action=delete&K=$row->ID">delete</a></TD>
	</TD>

EOD;
};
$s .= "</TABLE><br><a href=\"psa.kiosk.price.php?action=new\">new</a><br>";
ibase_free_result($h);
echo $s;
}
?>