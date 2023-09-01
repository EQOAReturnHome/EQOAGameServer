function event_say()
diagOptions = {}
    npcDialogue = "To the south is the Grand Amphitheater for some exhilarating entertainment or visit the Tranquil Gardens if you need a bit of quiet while you are with us."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end