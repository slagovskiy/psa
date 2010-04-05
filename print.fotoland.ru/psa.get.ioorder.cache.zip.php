<?php
include("config.inc");
include("psa.zip.php");

$timestamp = getdate();
$filename = $timestamp[0] . '.ioorder.xml';
$fp = fopen('/tmp/' . $filename, 'x');

$dbh = ibase_connect(DB, DBUSER, DBPASSWD ,"win1251");

$s .= "<?xml version=\"1.0\" encoding=\"windows-1251\"?>\n<IOORDER>\n";

$s .= "</IOORDER>\n";
fwrite($fp, $s);

ibase_close($dbh);

fclose($fp);

$zipTest = new zipfile();
$zipTest->add_file('/tmp/' . $filename, $filename);

Header("Content-type: application/octet-stream");
Header ("Content-disposition: attachment; filename=ioorder.xml.zip");
echo $zipTest->file();

unlink('/tmp/' . $filename);
?>
