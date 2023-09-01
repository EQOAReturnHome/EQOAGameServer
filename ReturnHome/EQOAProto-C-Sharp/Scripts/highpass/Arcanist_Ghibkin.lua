function  event_say(choice)
diagOptions = {}
    npcDialogue = "Not to worry.  Gnomicus Prime isn't aggressive.  He may look huge and menacing but he's actually pretty peaceful."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end