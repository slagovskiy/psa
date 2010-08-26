<?php
require("inc.db.php");

$key = $_GET["key"];
if ($key == "") $key = str_replace(' ', '', str_replace('0.', '', microtime()));

$format = $_GET["format"];
if ($format == "") $format = "xml";

$query = "SELECT `key`, `sdate`, `stime`, `cash`, `shift`, `check`, `sect`, `sign`, `code`, `discard`, `credcard`, `discsum`, `credsum`, `sum`, `qty`, `barcode`, `dgsum`, `dtsum`, `dssum`, `ddsum`, `cnum`, `user`, `price`, `del`, `commit` FROM `sales` ORDER BY `sdate`";
$result = mysql_query($query, $db);

if (!$result) 
{
    echo "DB Error, could not query the database\n";
    echo 'MySQL Error: ' . mysql_error();
    exit;
}
if ($format == 'xml')
{
	$s = "<?xml version=\"1.0\" encoding=\"windows-1251\"?>\n<DATA>\n";
	$s .= "\t<KEY>" . $key . "</KEY>\n";
	while ($row = mysql_fetch_assoc($result)) 
	{
		$s .= "\t<ROW>\n";
		$s .= "\t\t<KEY>" . $row['key'] . "</KEY>\n";
		$s .= "\t\t<DATE>" . $row['sdate'] . "</DATE>\n";
		$s .= "\t\t<TIME>" . $row['stime'] . "</TIME>\n";
		$s .= "\t\t<CACH>" . $row['cash'] . "</CACH>\n";
		$s .= "\t\t<SHIFT>" . $row['shift'] . "</SHIFT>\n";
		$s .= "\t\t<CHECK>" . $row['check'] . "</CHECK>\n";
		$s .= "\t\t<SECT>" . $row['sect'] . "</SECT>\n";
		$s .= "\t\t<SIGN>" . $row['sign'] . "</SIGN>\n";
		$s .= "\t\t<CODE>" . $row['code'] . "</CODE>\n";
		$s .= "\t\t<DISCARD>" . $row['discard'] . "</DISCARD>\n";
		$s .= "\t\t<CREDCARD>" . $row['credcard'] . "</CREDCARD>\n";
		$s .= "\t\t<DISCSUM>" . $row['discsum'] . "</DISCSUM>\n";
		$s .= "\t\t<CREDSUM>" . $row['credsum'] . "</CREDSUM>\n";
		$s .= "\t\t<SUM>" . $row['sum'] . "</SUM>\n";
		$s .= "\t\t<QTY>" . $row['qty'] . "</QTY>\n";
		$s .= "\t\t<BARCODE>" . $row['barcode'] . "</BARCODE>\n";
		$s .= "\t\t<DGSUM>" . $row['dgsum'] . "</DGSUM>\n";
		$s .= "\t\t<DTSUM>" . $row['dtsum'] . "</DTSUM>\n";
		$s .= "\t\t<DSSUM>" . $row['dssum'] . "</DSSUM>\n";
		$s .= "\t\t<DDSUM>" . $row['ddsum'] . "</DDSUM>\n";
		$s .= "\t\t<CNUM>" . $row['cnum'] . "</CNUM>\n";
		$s .= "\t\t<USER>" . $row['user'] . "</USER>\n";
		$s .= "\t\t<PRICE>" . $row['price'] . "</PRICE>\n";
		$s .= "\t\t<DEL>" . ((ord($row['del'])==1)?"1":"0") . "</DEL>\n";
		$s .= "\t\t<COMMIT>" . ((ord($row['commit'])==1)?"1":"0") . "</COMMIT>\n";
		$s .= "\t</ROW>\n";
	}
	$s .= "</DATA>";
	echo $s;
}
else if ($format == "csv")
{
	$s = "key;date;time;cash;shift;check;sect;sign;code;discard;credcard;discsum;credsum;sum;qty;barcode;dgsum;dtsum;dssum;ddsum;cnum;user;price;del;commit\n";
	while ($row = mysql_fetch_assoc($result)) 
	{
		$bonus = $row['bonus'];
		$subquery = "SELECT `bonus` FROM `card_action` WHERE `code` = '" . $code . "' AND `del` = 0 AND `commit` = 1";
		$subresult = mysql_query($subquery, $db);
		while ($subrow = mysql_fetch_assoc($subresult))
		{
			$bonus += $subrow['bonus'];
		}
		//echo $row['code'];
		$s .= $key . ";";
		$s .= $row['key'] . ";";
		$s .= $row['sdate'] . ";";
		$s .= $row['stime'] . ";";
		$s .= $row['cash'] . ";";
		$s .= $row['shift'] . ";";
		$s .= $row['check'] . ";";
		$s .= $row['sect'] . ";";
		$s .= $row['sign'] . ";";
		$s .= $row['code'] . ";";
		$s .= $row['discard'] . ";";
		$s .= $row['credcard'] . ";";
		$s .= $row['discsum'] . ";";
		$s .= $row['credsum'] . ";";
		$s .= $row['sum'] . ";";
		$s .= $row['qty'] . ";";
		$s .= $row['barcode'] . ";";
		$s .= $row['dgsum'] . ";";
		$s .= $row['dtsum'] . ";";
		$s .= $row['dssum'] . ";";
		$s .= $row['ddsum'] . ";";
		$s .= $row['cnum'] . ";";
		$s .= $row['user'] . ";";
		$s .= $row['price'] . ";";
		$s .= ((ord($row['del'])==1)?"1":"0") . ";";
		$s .= ((ord($row['commit'])==1)?"1":"0") . "\n";
	}
	echo $s;
}
else
{
	echo "Error: Format not found.";
}
?>