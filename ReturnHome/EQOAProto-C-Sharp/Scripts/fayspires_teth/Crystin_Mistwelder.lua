function event_say()
diagOptions = {}
    npcDialogue = "Good day to thee, traveler. May Tunare find you in time, and may she guide you all to a conclusion of peace."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end