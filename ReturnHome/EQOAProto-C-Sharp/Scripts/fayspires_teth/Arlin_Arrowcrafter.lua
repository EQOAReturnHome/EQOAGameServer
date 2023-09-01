function event_say()
diagOptions = {}
    npcDialogue = "Though a true ranger becomes an expert with the bow and arrow, they must still be prepared to fight in close quarters. That is why we train all rangers with a Guardian's Blade. A sword that can be quickly drawn when the enemy closes in, and sheathed with haste when the battle demands a rangers arrow."
SendDialogue(mySession, npcDialogue, diagOptions)
end