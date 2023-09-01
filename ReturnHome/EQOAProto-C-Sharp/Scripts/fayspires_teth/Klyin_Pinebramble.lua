function event_say()
diagOptions = {}
    npcDialogue = "Greetings, traveler. May the light of Tunare find and guide thee well when the darkness seems impenetrable."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end