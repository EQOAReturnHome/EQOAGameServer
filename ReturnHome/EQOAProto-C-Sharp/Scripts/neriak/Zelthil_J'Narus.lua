function  event_say(choice)
diagOptions = {}
    npcDialogue = "playername, have you ever been to the city of Freeport to the south? A genuine place accepting of the shadows. A place even us Dark Elves inhabit. Maybe you should consider traveling there."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end