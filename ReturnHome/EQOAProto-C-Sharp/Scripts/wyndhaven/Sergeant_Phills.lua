function  event_say(choice)
diagOptions = {}
    npcDialogue = "Those bandits in the east ruins are too organized to be the usual mindless riffraff of thieves. Someone is giving them specific objectives to fulfil."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end