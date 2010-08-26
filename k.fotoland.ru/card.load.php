<?php
require("inc.db.php");

$lines = file("dcard.csv");
$query = "";
$i = 1;
foreach($lines as $line)
{
	list($code, $name, $disc, $discserv, $phone, $email, $bonus, $typebonus) = split(';', $line);
    $query = "INSERT INTO `card` (`code`, `name`, `disc`, `discserv`, `phone`, `email`, `bonus`, `typebonus`) VALUES ('" . $code . "', '" . $name . "', " . str_replace(',', '.', $disc) . ", " . str_replace(',', '.', $discserv) . ", '" . $phone . "', '" . $email . "', " . str_replace(',', '.', $bonus) . ", '" . $typebonus . "')";
	$result = mysql_query($query, $db);
	if(!$result)
	{
		echo "Error(2): could not query the database at line " . $i . "\n";
		echo "MySQL Error: " . mysql_error();
		mysql_query("DELETE FROM `card` WHERE `commit` = 0", $db);
		exit;
	}
	$i++;
	//echo $query . "<br>";
}

mysql_query("DELETE FROM `card` WHERE `commit` = 1", $db);
mysql_query("UPDATE `card` SET `commit` = 1 WHERE `commit` = 0", $db);
echo "OK";

//TRUNCATE TABLE `card` 

?>