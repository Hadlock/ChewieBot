<?php

error_reporting(E_ALL);

define("MAX_USERNAME_LENGTH", 16);

if (isset($_GET['lolocaust']))
{
		define("FILE_VIPLIST", "viplist");
		define("FILE_QUEUE", "queue");
		define("FILE_KICKREASONS", "kickreasons");
}
else
{
		define("FILE_VIPLIST", "viplist");
		define("FILE_QUEUE", "queue");
		define("FILE_KICKREASONS", "kickreasons");
}		


function get_kickreasons()
{
	return array_map("trim", file(FILE_KICKREASONS, FILE_SKIP_EMPTY_LINES));
}

function push_kickreasons()
{	
	/* fail on duplicate */
	if (in_array($_POST['newReason'], get_kickreasons()))
		return false;
	
	if (!empty($_POST['newReason']) && strlen($_POST['newReason'] <= 80))
	{
		$newReason = stripslashes(htmlspecialchars($_POST['newReason']));
		file_put_contents(FILE_KICKREASONS, $newReason . "\n", FILE_APPEND | LOCK_EX);
		
		return true;
	}
	else
	{
		return false;
	}
}

/* List of names on VIP list, separated by newline */
function get_vip_list()
{
	return array_map("trim", file(FILE_VIPLIST, FILE_SKIP_EMPTY_LINES));
}

/* List of names waiting to be added to VIP list, separated by newline */
function get_queue_list()
{
	return array_map("trim", file(FILE_QUEUE, FILE_SKIP_EMPTY_LINES));
}

/* Add a name to the queue */
function push_queue()
{

/*WARNING! HACK! HACK! HACK! Added 41ch as a hardcoded name to get around the name type error that breaks names starting with 17 or higher*/

	/*if ((!empty($_POST['username']) && strlen($_POST['username'] <= MAX_USERNAME_LENGTH)) || $_POST['username'] == "41ch")*/
	if (!empty($_POST['username']) && (strlen($_POST['username']) <= MAX_USERNAME_LENGTH))

	{
		$username = htmlspecialchars($_POST['username']);
		file_put_contents(FILE_QUEUE, $username . "\n", FILE_APPEND | LOCK_EX);
		
		return true;
	}
	else
	{
		return false;
	}
}

/*
	Merge the queue and VIP list, and return the new list.
	We don't sort here because we want newer members to go to the top of the list due to BC2 limitations.
        This part is probably irrelevant if you're reading this after 10/25/2011
*/
function merge_lists()
{

	/* Get current VIP list and queue list data */
	$vips = get_vip_list();
	$queued = get_queue_list();

	foreach ($queued as $q)
	{
		/* New members go to the top of the list */
		array_unshift($vips, $q);
	}

	/* Get rid of duplicates */
	$vips = array_unique($vips);
	
	/* Avoid writing an empty VIP list */
	if (empty($vips) || count($vips) < 1)
		return false;
	
	/* Wipe the old VIP list */
	file_put_contents(FILE_VIPLIST, null);
	
	/* Write the new VIP list, one player at a time */
	foreach ($vips as $v)
	{
		if (file_put_contents(FILE_VIPLIST, $v . "\n", FILE_APPEND) == false)
			return false;
	}
	
	file_put_contents(FILE_QUEUE, null);
		
	return get_vip_list();
}

function lolocaust_set_lastrun()
{
	file_put_contents("lolocaust_lastrun", time());
}

function lolocaust_get_lastrun()
{
	return file_get_contents("lolocaust_lastrun");
}

function lolocaust_init()
{
	/* Sufficient time has passed, continue */
	$oldTime = intval(lolocaust_get_lastrun());
	if ((time() - $oldTime) > 60)
	{
		lolocaust_set_lastrun();
		
		return true;
	}
	/* We're only allowed to run once per minute */
	else
	{	
		return false;
	}
}

?>