function  event_say(choice)
diagOptions = {}
    npcDialogue = "Councilor Nothard is just upstairs, if you have official business with him."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end