<?php

$_GET['lolocaust'] = true;
require_once("funcs.php");

if (isset($_POST['submit']))
{
	if (!push_kickreasons($_POST['newReason']))
	{
		die("there was a terrible problem with your terrible input, try again (also duplicate entries not accepted)");
	}
}

?>

<br />This is the list of kick reasons that someone will see when !kick kicks them out of the server. Randomly chosen.<br /><br />

<form method="post" action="index-old.php">
	<input type="text" name="newReason" maxlength="80" /> kick reason (max length 80 characters)<br />
	<input type="submit" name="submit" value="submit" />
</form>

<?php

	$reasons = get_kickreasons();
	sort($reasons);
	
	$count = count($reasons);
	
	echo "<strong>Total: " . ($count + 1) . "</strong><br /><br /><ul>";
	
	foreach ($reasons as $reason)
	{
		echo "<li />" .$reason. "<br />";
	}	
	
	echo "</ul>";

?>