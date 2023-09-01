function  event_say(choice)
diagOptions = {}
    npcDialogue = "Be careful carrying that much tunar around town�might attract some unwanted attention from certain folk."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end