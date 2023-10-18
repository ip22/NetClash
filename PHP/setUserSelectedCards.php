<?php
    require '../database.php';

    $userID = $_POST['userID'];
    $newSelectedCards = $_POST['newSelectedIDs'];

    if(!isset($userID)){
        echo 'Client data error';
        exit;
    }

    $user = R::load('users', $userID);
    $selectedID = json_decode($newSelectedCards);

    $links = $user -> ownCardsUsers;

    foreach($links as $link)
    {
        $link -> selected = false;
    }

    foreach($links as $link)
    {
        $link -> selected = in_array($link["cards_id"], $selectedID, true);
    }

    R::store($user);

    echo 'save ok';
?>