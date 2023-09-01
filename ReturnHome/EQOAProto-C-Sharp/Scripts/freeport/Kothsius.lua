function  event_say(choice)
diagOptions = {}
    npcDialogue = "Get away from me before my blade leaves you eviscerated!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end