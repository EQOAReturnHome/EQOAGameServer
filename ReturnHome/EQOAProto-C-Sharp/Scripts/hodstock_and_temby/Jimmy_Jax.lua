function  event_say(choice)
diagOptions = {}
    npcDialogue = "I'm just waiting for a shipment. Boat shoud be arriving any time."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end