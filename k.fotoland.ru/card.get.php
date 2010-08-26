<?php
require("inc.db.php");

$key = $_GET["key"];
if ($key == "") $key = str_replace(' ', '', str_replace('0.', '', microtime()));

$code = $_GET["code"];
if ($code == "") $code = "0";
if (strlen($code) > 15) $code = "0";

$format = $_GET["format"];
if ($format == "") $format = "xml";


$query = "SELECT `code`, `name`, `disc`, `discserv`, `phone`, `email`, `bonus`, `typebonus` FROM `card` WHERE `code` = '" . $code . "' AND `commit` = 1";
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
	if ($row = mysql_fetch_assoc($result)) 
	{
		$bonus = $row['bonus'];
		$subquery = "SELECT `bonus` FROM `card_action` WHERE `code` = '" . $code . "' AND `del` = 0 AND `commit` = 1";
		$subresult = mysql_query($subquery, $db);
		while ($subrow = mysql_fetch_assoc($subresult))
		{
			$bonus += $subrow['bonus'];
		}
		//echo $row['code'];
		$s .= "\t<CARD>\n";
		$s .= "\t\t<CODE>" . $row['code'] . "</CODE>\n";
		$s .= "\t\t<NAME>" . $row['name'] . "</NAME>\n";
		$s .= "\t\t<DISC>" . $row['disc'] . "</DISC>\n";
		$s .= "\t\t<DISCSERV>" . $row['discserv'] . "</DISCSERV>\n";
		$s .= "\t\t<PHONE>" . $row['phone'] . "</PHONE>\n";
		$s .= "\t\t<EMAIL>" . $row['email'] . "</EMAIL>\n";
		$s .= "\t\t<BONUS>" . $bonus . "</BONUS>\n";
		$s .= "\t\t<TYPE>" . str_replace('\n', '', $row['typebonus']) . "</TYPE>\n";
		$s .= "\t</CARD>\n";
		$s .= "</DATA>";
		echo $s;
	}
	else
	{
		echo "Error: card not found.\n";
		exit;
	}
}
else if ($format == "csv")
{
	$s = "key;code;name;disc;discserv;phone;email;bonus;type\n";
	if ($row = mysql_fetch_assoc($result)) 
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
		$s .= $row['code'] . ";";
		$s .= $row['name'] . ";";
		$s .= $row['disc'] . ";";
		$s .= $row['discserv'] . ";";
		$s .= $row['phone'] . ";";
		$s .= $row['email'] . ";";
		$s .= $bonus . ";";
		$s .= $row['typebonus'] . "\n";
		echo $s;
	}
	else
	{
		echo "Error: card not found.\n";
		exit;
	}
}
else
{
	echo "Error: Format not found.";
}
?>