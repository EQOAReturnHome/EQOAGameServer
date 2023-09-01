function  event_say(choice)
diagOptions = {}
    npcDialogue = "You've no business within my home. Leave at once!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end