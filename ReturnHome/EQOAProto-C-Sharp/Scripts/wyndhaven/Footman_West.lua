function  event_say(choice)
diagOptions = {}
    npcDialogue = "No time to chat, playerName. The boss wants this scout report done yesterday!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end