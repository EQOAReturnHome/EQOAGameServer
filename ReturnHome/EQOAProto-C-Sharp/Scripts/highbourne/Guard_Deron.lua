function event_say()
diagOptions = {}
    npcDialogue = "Welcome to Highbourne, adventurer. Just inside the East Gate you will find the Coach of Highbourne and a plethora of merchants to resupply for your departure."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end