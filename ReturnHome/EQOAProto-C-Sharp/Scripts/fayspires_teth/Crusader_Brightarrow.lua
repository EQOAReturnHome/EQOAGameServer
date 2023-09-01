function event_say()
diagOptions = {}
    npcDialogue = "Trainer Dolby is often found traveling the road on the way to Tethelin. He is quite knowledgeable when is comes to the local wildlife, and exotic components found in the area."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end