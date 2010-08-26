<?php
echo str_replace(' ', '', str_replace('.', '', $_SERVER['REMOTE_ADDR'] . microtime()));
?>