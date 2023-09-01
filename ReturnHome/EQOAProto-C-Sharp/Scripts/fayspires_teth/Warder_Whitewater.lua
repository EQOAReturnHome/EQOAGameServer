function event_say()
diagOptions = {}
    npcDialogue = "Be at ease among us, friends. Tethelin's guards are not to be underestimated."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end