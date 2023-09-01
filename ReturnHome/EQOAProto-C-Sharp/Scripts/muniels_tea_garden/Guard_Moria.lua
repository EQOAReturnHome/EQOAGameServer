function event_say()
diagOptions = {}
    npcDialogue = "Be kind to Farn. I think he is a bit homesick and the pies here aren't exactly...delicious."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end