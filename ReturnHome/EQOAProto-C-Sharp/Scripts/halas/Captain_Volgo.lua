function  event_say(choice)
diagOptions = {}
    npcDialogue = "Those blasted giants! It seems like this happens the same time every year."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end