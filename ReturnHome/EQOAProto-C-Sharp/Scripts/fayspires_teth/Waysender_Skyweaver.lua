function event_say()
diagOptions = {}
    npcDialogue = "The far reaches of the world aren't as far as it would seem. I may send you to these places, but only at the wisdom of your guildmaster."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end