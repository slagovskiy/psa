<?php
require("inc.db.php");

$lines = file("bsaldo.csv");
mysql_query('TRUNCATE TABLE `bsaldo`', $db);
$query = "";
$i = 1;
foreach($lines as $line)
{
	$line = rtrim(str_replace("'", "\'", $line), "\r\n");
	list($code, $barcode, $num, $name, $part, $price, $unit, $serv, $enum, $nodisc, $noret, $qty, $sect) = split(';', $line);
    $query = "INSERT INTO `bsaldo` (`code`, `barcode`, `num`, `name`, `part`, `price`, `unit`, `serv`, `enum`, `nodisc`, `noret`, `qty`, `sect`) VALUES ('" . $code . "', '" . $barcode . "', '" . $num . "', '" . $name . "', '" . $part . "', " . str_replace(',', '.', $price) . ", '" . $unit . "', '" . $serv . "', '" . $enum . "', '" . $nodisc . "', '" . $noret . "', " . str_replace(',', '.', $qty) . ", " . $sect . ")";
	$result = mysql_query($query, $db);
	if(!$result)
	{
		echo "Error(2): could not query the database at line " . $i . "\n";
		echo "MySQL Error: " . mysql_error();
		mysql_query("DELETE FROM `bsaldo` WHERE `commit` = 0", $db);
		exit;
	}
	$i++;
	//echo $query . "<br>";
}

mysql_query("DELETE FROM `bsaldo` WHERE `commit` = 1", $db);
mysql_query("UPDATE `bsaldo` SET `commit` = 1 WHERE `commit` = 0", $db);
echo "OK";

//TRUNCATE TABLE `card` 

?>