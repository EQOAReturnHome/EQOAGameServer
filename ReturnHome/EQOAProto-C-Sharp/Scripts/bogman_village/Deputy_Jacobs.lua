function  event_say(choice)
diagOptions = {}
    npcDialogue = "What business could you possibly have here? Have you seen the undead roaming the beach? You'll end up like them, no doubt."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end