<?php
require("inc.db.php");

$key = $_GET["key"];
if ($key == "") $key = str_replace(' ', '', str_replace('0.', '', microtime()));

$format = $_GET["format"];
if ($format == "") $format = "xml";

$query = "SELECT `id`, `date`, `code`, `key`, `bonus`, `commit`, `del` FROM `card_action` WHERE `commit` = 1 AND `del` = 0 ORDER BY `date`";
$result = mysql_query($query, $db);

$fname = "dcard-" . date("Ymd-His.") . $format;

$fh = fopen("export/" . $fname, 'w') or die("can't open file " . $fname);

$ids = "0";

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
	$s .= "\t<FILE>" . $fname . "</FILE>\n";
	while ($row = mysql_fetch_assoc($result)) 
	{
		$s .= "\t<ACTION>\n";
		$s .= "\t\t<ID>" . $row['id'] . "</ID>\n";
		$s .= "\t\t<DATE>" . $row['date'] . "</DATE>\n";
		$s .= "\t\t<CODE>" . $row['code'] . "</CODE>\n";
		$s .= "\t\t<KEY>" . $row['key'] . "</KEY>\n";
		$s .= "\t\t<BONUS>" . $row['bonus'] . "</BONUS>\n";
		$s .= "\t\t<COMMIT>" . ((ord($row['commit'])==1)?"1":"0") . "</COMMIT>\n";
		$s .= "\t\t<DEL>" . ((ord($row['del'])==1)?"1":"0") . "</DEL>\n";
		$s .= "\t</ACTION>\n";
		$ids .= ", " . $row['id'];
	}
	$s .= "</DATA>";
	echo $s;
	fwrite($fh, $s);
	fclose($fh);
	$query = "UPDATE `card_action` SET `del` = 1 WHERE `id` IN (" . $ids . ")";
	$result = mysql_query($query, $db);
}
else if ($format == "csv")
{
	$s = "id;date;code;key;bonus;commit;del;file\n";
	while ($row = mysql_fetch_assoc($result)) 
	{
		$s .= $row['id'] . ";";
		$s .= $row['date'] . ";";
		$s .= $row['code'] . ";";
		$s .= $row['key'] . ";";
		$s .= $row['bonus'] . ";";
		$s .= ((ord($row['commit'])==1)?"1":"0") . ";";
		$s .= ((ord($row['del'])==1)?"1":"0") . ";";
		$s .= $fname . "\n";
		$ids .= ", " . $row['id'];
	}
	echo $s;
	fwrite($fh, $s);
	fclose($fh);
	$query = "UPDATE `card_action` SET `del` = 1 WHERE `id` IN (" . $ids . ")";
	$result = mysql_query($query, $db);
}
else
{
	echo "Error: Format not found.";
}
?>