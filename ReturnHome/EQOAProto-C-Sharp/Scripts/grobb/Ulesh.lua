function  event_say(choice)
diagOptions = {}
    npcDialogue = "Ulesh here for best Grobb meal. Ulesh very hungry. Don't bother Ulesh, or you may get eaten when you aren't looking."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end