function event_say()
diagOptions = {}
    npcDialogue = "Our recipes have been handed down through generations of Greenwaters. They are a special combination of geomancer and druidic arts."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end