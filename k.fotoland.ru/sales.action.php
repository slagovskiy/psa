<?php
require("inc.db.php");

$date = $_GET["date"];
if ($date == "") $date = date("Y/m/d");

$time = $_GET["time"];
if ($time == "") $time = date("H:i:s");

$cash = $_GET["cash"];
if ($cash == "") $cash = 0;

$shift = $_GET["shift"];
if ($shift == "") $shift = 0;

$check = $_GET["check"];
if ($check == "") $check = 0;

$sect = $_GET["sect"];
if ($sect == "") $sect = 0;

$sign = $_GET["sign"];
if ($sign == "") $sign = 0;

$code = $_GET["code"];
if ($code == "") $code = "";

$discard = $_GET["discard"];
if ($discard == "") $discard = "";

$credcard = $_GET["credcard"];
if ($credcard == "") $credcard = "";

$discsum = $_GET["discsum"];
if ($discsum == "") $discsum = 0;

$credsum = $_GET["credsum"];
if ($credsum == "") $credsum = 0;

$sum = $_GET["sum"];
if ($sum == "") $sum = 0;

$qty = $_GET["qty"];
if ($qty == "") $qty = 0;

$barcode = $_GET["barcode"];
if ($barcode == "") $barcode = "";

$dgsum = $_GET["dgsum"];
if ($dgsum == "") $dgsum = 0;

$dtsum = $_GET["dtsum"];
if ($dtsum == "") $dtsum = 0;

$dssum = $_GET["dssum"];
if ($dssum == "") $dssum = 0;

$ddsum = $_GET["ddsum"];
if ($ddsum == "") $ddsum = 0;

$cnum = $_GET["cnum"];
if ($cnum == "") $cnum = 0;

$user = $_GET["user"];
if ($user == "") $user = "";

$price = $_GET["price"];
if ($price == "") $price = 0;

$key = $_GET["key"];
if ($key == "") $key = str_replace(' ', '', str_replace('0.', '', microtime()));

$action = $_GET["action"];
if ($action == "") 
{
	echo "Error: Action not found.\n";
	exit;
}

if ($action == "add")
{
	$query = "INSERT INTO `sales` (`key`, `sdate`, `stime`, `cash`, `shift`, `check`, `sect`, `sign`, `code`, `discard`, `credcard`, `discsum`, `credsum`, `sum`, `qty`, `barcode`, `dgsum`, `dtsum`, `dssum`, `ddsum`, `cnum`, `user`, `price`, `del`, `commit`) VALUES ('" . $key . "', '" . $date . "', '" . $time . "', " . $cash . ", " . $shift . ", " . $check . ", " . $sect . ", " . $sign . ", '" . $code . "', '" . $discard . "', '" . $credcard . "', " . $discsum . ", " . $credsum . ", " . $sum . ", " . $qty . ", '" . $barcode . "', " . $dgsum . ", " . $dtsum . ", " . $dssum . ", " . $ddsum . ", " . $cnum . ", '" . $user . "', " . $price . ", 0, 0)";
	$result = mysql_query($query, $db);
	if(!$result)
	{
		echo "DB Error, could not query the database\n";
		echo 'MySQL Error: ' . mysql_error();
		exit;
	}
	else
	{
		echo "OK. Confirm, please.";
	}
}
else if ($action == "confirm")
{
	$query = "SELECT count(*) as `cnt` FROM `sales` WHERE `sdate` = '" . $date . "' AND `stime` = '" . $time . "' AND `cash` = " . str_replace(",", ".", $cash) . " AND `shift` = " . str_replace(",", ".", $shift) . " AND `check` = " . str_replace(",", ".", $check) . " AND `sect` = " . str_replace(",", ".", $sect) . " AND `sign` = " . str_replace(",", ".", $sign) . " AND `code` = '" . $code . "' AND `discard` = '" . $discard . "' AND `credcard` = '" . $credcard . "' AND `discsum` = " . str_replace(",", ".", $discsum) . " AND `credsum` = " . str_replace(",", ".", $credsum) . " AND `sum` = " . str_replace(",", ".", $sum) . " AND `qty` = " . str_replace(",", ".", $qty) . " AND `barcode` = '" . $barcode . "' AND `dgsum` = " . str_replace(",", ".", $dgsum) . " AND `dtsum` = " . str_replace(",", ".", $dtsum) . " AND `dssum` = " . str_replace(",", ".", $dssum) . " AND `ddsum` = " . str_replace(",", ".", $ddsum) . " AND `cnum` = " . str_replace(",", ".", $cnum) . " AND `user` = '" . $user . "' AND `price` = " . str_replace(",", ".", $price) . " AND `del` = 0 AND `commit` = 0";
	$result = mysql_query($query, $db);
	if (!$result) 
	{
		echo "DB Error, could not query the database\n";
		echo 'MySQL Error: ' . mysql_error();
		exit;
	}

	if($row = mysql_fetch_assoc($result))
	{
		if ($row['cnt'] != 1)
		{
			echo "Error: Error adding information about the sale, repeat the procedure with the new key.\n";
			exit;
		}
		else
		{
			$query = "UPDATE `sales` SET `commit` = 1 WHERE `sdate` = '" . $date . "' AND `stime` = '" . $time . "' AND `cash` = " . str_replace(",", ".", $cash) . " AND `shift` = " . str_replace(",", ".", $shift) . " AND `check` = " . str_replace(",", ".", $check) . " AND `sect` = " . str_replace(",", ".", $sect) . " AND `sign` = " . str_replace(",", ".", $sign) . " AND `code` = '" . $code . "' AND `discard` = '" . $discard . "' AND `credcard` = '" . $credcard . "' AND `discsum` = " . str_replace(",", ".", $discsum) . " AND `credsum` = " . str_replace(",", ".", $credsum) . " AND `sum` = " . str_replace(",", ".", $sum) . " AND `qty` = " . str_replace(",", ".", $qty) . " AND `barcode` = '" . $barcode . "' AND `dgsum` = " . str_replace(",", ".", $dgsum) . " AND `dtsum` = " . str_replace(",", ".", $dtsum) . " AND `dssum` = " . str_replace(",", ".", $dssum) . " AND `ddsum` = " . str_replace(",", ".", $ddsum) . " AND `cnum` = " . str_replace(",", ".", $cnum) . " AND `user` = '" . $user . "' AND `price` = " . str_replace(",", ".", $price) . " AND `del` = 0 AND `commit` = 0";
			$result = mysql_query($query, $db);
			if(!$result)
			{
				echo "DB Error, could not query the database\n";
				echo 'MySQL Error: ' . mysql_error();
				exit;
			}
			else
			{
				echo "OK.";
			}
		}
	}
}
else
{
	echo "Error: Action not found.\n";
	exit;
}

?>