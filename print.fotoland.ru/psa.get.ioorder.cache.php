<?php
include("config.inc");
$dbh = ibase_connect(DB, DBUSER, DBPASSWD ,"win1251");

$s .= "<?xml version=\"1.0\" encoding=\"windows-1251\"?>\n<IOORDER>\n";

$s .= "</IOORDER>\n";
echo $s;

ibase_close($dbh);
?>