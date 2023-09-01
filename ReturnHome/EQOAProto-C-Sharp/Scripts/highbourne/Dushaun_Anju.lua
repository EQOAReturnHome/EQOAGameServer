function event_say()
diagOptions = {}
    npcDialogue = "I must have the horn of silence…whatever it takes. I know the horn will further my studies."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end