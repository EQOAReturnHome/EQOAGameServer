function  event_say(choice)
diagOptions = {}
    npcDialogue = "Please keep it brief, Councilor Nothard has many duties to attend to."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end