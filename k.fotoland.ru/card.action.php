<?php
require("inc.db.php");

$key = $_GET["key"];
if ($key == "") $key = str_replace(' ', '', str_replace('0.', '', microtime()));

$bonus = $_GET["bonus"];
if ($bonus == "") $bonus = 0;

$code = $_GET["code"];
if ($code == "") $code = "0";
if (strlen($code) > 15) $code = "0";

$action = $_GET["action"];
if ($action == "") 
{
	echo "Error: Action not found.\n";
	exit;
}

if ($action == "add")
{
	$cbonus = 0;
	$query = "SELECT `bonus` FROM `card` WHERE `code` = '" . $code . "' AND `commit` = 1";
	$result = mysql_query($query, $db);

	if (!$result) 
	{
		echo "DB Error, could not query the database\n";
		echo 'MySQL Error: ' . mysql_error();
		exit;
	}
	if ($row = mysql_fetch_assoc($result)) 
	{
		$cbonus = $row['bonus'];
		$subquery = "SELECT `bonus` FROM `card_action` WHERE `code` = '" . $code . "' AND `del` = 0 AND `commit` = 1";
		$subresult = mysql_query($subquery, $db);
		while ($subrow = mysql_fetch_assoc($subresult))
		{
			$cbonus += $subrow['bonus'];
		}
	}
	else
	{
		echo "Error: Card not found.\n";
		exit;
	}
	if ($cbonus >= $bonus)
	{
		$query = "INSERT INTO `card_action` (`key`, `code`, `bonus`, `commit`, `del`) VALUES ('" . $key . "', '" . $code . "', " . $bonus . ", 0, 0)";
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
	else
	{
		echo "Error: Not enough bonuses.\n";
		exit;
	}
}
else if ($action == "confirm")
{
	$query = "SELECT count(*) as `cnt` FROM `card_action` WHERE `code` = '" . $code . "' AND `del` = 0 AND `commit` = 0 AND `key` = '" . $key . "'";
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
			echo "Error: Error deducting bonuses, repeat the procedure with the new key.\n";
			exit;
		}
		else
		{
			$query = "UPDATE `card_action` SET `commit` = 1 WHERE `code` = '" . $code . "' AND `del` = 0 AND `commit` = 0 AND `key` = '" . $key . "'";
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