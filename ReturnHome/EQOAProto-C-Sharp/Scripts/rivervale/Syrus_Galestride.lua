function  event_say(choice)
diagOptions = {}
    npcDialogue = "That damned goblin is a crafty one. He'll slip up one of these days though and I'll have some adventurers ready when that time comes."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end