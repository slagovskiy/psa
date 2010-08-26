<?php
require("inc.db.php");

$key = $_GET["key"];
if ($key == "") $key = str_replace(' ', '', str_replace('0.', '', microtime()));

$format = $_GET["format"];
if ($format == "") $format = "xml";


$query = "SELECT `code`, `barcode`, `num`, `name`, `part`, `price`, `unit`, `serv`, `enum`, `nodisc`, `noret`, `qty`, `sect` FROM `bsaldo` WHERE `commit` = 1 ORDER BY `code`";
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
		$s .= "\t\t<CODE>" . $row['code'] . "</CODE>\n";
		$s .= "\t\t<BARCODE>" . $row['barcode'] . "</BARCODE>\n";
		$s .= "\t\t<NUM>" . $row['num'] . "</NUM>\n";
		$s .= "\t\t<NAME>" . $row['name'] . "</NAME>\n";
		$s .= "\t\t<PART>" . $row['part'] . "</PART>\n";
		$s .= "\t\t<PRICE>" . $row['price'] . "</PRICE>\n";
		$s .= "\t\t<UNIT>" . $row['unit'] . "</UNIT>\n";
		$s .= "\t\t<SERV>" . $row['serv'] . "</SERV>\n";
		$s .= "\t\t<ENUM>" . $row['enum'] . "</ENUM>\n";
		$s .= "\t\t<NODISC>" . $row['nodisc'] . "</NODISC>\n";
		$s .= "\t\t<NORET>" . $row['noret'] . "</NORET>\n";
		$s .= "\t\t<QTY>" . $row['qty'] . "</QTY>\n";
		$s .= "\t\t<SECT>" . $row['sect'] . "</SECT>\n";
		$s .= "\t</ROW>\n";
	}
	$s .= "</DATA>";
	echo $s;
}
else if ($format == "csv")
{
	$s = "key;code;barcode;num;name;part;price;unit;serv;enum;nodisc;noret;qty;sect\n";
	while ($row = mysql_fetch_assoc($result)) 
	{
		$s .= $key . ";";
		$s .= $row['code'] . ";";
		$s .= $row['barcode'] . ";";
		$s .= $row['num'] . ";";
		$s .= $row['name'] . ";";
		$s .= $row['part'] . ";";
		$s .= $row['price'] . ";";
		$s .= $row['unit'] . ";";
		$s .= $row['serv'] . ";";
		$s .= $row['enum'] . ";";
		$s .= $row['nodisc'] . ";";
		$s .= $row['noret'] . ";";
		$s .= $row['qty'] . ";";
		$s .= $row['sect'] . "\n";
	}
	echo $s;
}
else
{
	echo "Error: Format not found.";
}
?>