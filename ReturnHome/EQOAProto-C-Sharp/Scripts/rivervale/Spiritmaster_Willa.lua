function  event_say(choice)
diagOptions = {}
    npcDialogue = "If you wish, I can bind your spirit to this place. Your body and possessions will rematerialize here but at a terrible price."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end