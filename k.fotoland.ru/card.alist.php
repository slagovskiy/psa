<?php
require("inc.db.php");

$key = $_GET["key"];
if ($key == "") $key = str_replace(' ', '', str_replace('0.', '', microtime()));

$format = $_GET["format"];
if ($format == "") $format = "xml";


$query = "SELECT `id`, `date`, `code`, `key`, `bonus`, `commit`, `del` FROM `card_action` ORDER BY `date`";
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
		$s .= "\t<ACTION>\n";
		$s .= "\t\t<ID>" . $row['id'] . "</ID>\n";
		$s .= "\t\t<DATE>" . $row['date'] . "</DATE>\n";
		$s .= "\t\t<CODE>" . $row['code'] . "</CODE>\n";
		$s .= "\t\t<KEY>" . $row['key'] . "</KEY>\n";
		$s .= "\t\t<BONUS>" . $row['bonus'] . "</BONUS>\n";
		$s .= "\t\t<COMMIT>" . $row['commit'] . "</COMMIT>\n";
		$s .= "\t\t<DEL>" . $row['del'] . "</DEL>\n";
		$s .= "\t</ACTION>\n";
	}
	$s .= "</DATA>";
	echo $s;
}
else if ($format == "csv")
{
	$s = "id;date;code;key;bonus;commit;del\n";
	while ($row = mysql_fetch_assoc($result)) 
	{
		$s .= $row['id'] . ";";
		$s .= $row['date'] . ";";
		$s .= $row['code'] . ";";
		$s .= $row['key'] . ";";
		$s .= $row['bonus'] . ";";
		$s .= $row['commit'] . ";";
		$s .= $row['del'] . "\n";
	}
	echo $s;
}
else
{
	echo "Error: Format not found.";
}
?>