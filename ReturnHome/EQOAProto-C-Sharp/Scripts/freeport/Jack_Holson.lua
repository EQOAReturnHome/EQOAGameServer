function  event_say(choice)
diagOptions = {}
    npcDialogue = "These vermin are ruining my business! "
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end