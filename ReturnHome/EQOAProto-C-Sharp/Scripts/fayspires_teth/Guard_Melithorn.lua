function event_say()
diagOptions = {}
    npcDialogue = "My wife was slaughtered by those wretched Hatebone Orcs. I have since given my life in service of cleansing this land of that vile race. I look forward to each day that Sir Lythilar sends us to thin their numbers."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end