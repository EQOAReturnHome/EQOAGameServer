function  event_say(choice)
diagOptions = {}
    npcDialogue = "I have the power to send you to far away places in a dash of lightning, playerName. But only with special permission from your guildmaster."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end