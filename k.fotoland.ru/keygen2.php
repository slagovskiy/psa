<?php
if ($_SERVER['REMOTE_ADDR'] == "192.168.1.235")
	$ip = $_SERVER['HTTP_X_REAL_IP'];
else
	$ip = $_SERVER['REMOTE_ADDR'];
echo str_replace(' ', '', str_replace('.', '', $ip . microtime()));

foreach($_SERVER as $key => $value) 
{
	echo $key . " = " . $value . "<hr>";
}
?>