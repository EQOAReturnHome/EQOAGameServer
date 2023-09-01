function event_say()
diagOptions = {}
    npcDialogue = "Hello, playerName. As guildmaster of alchemy here at the College of High Magic, I require all of my pupils to go through rigorous safety training before handling any explosive chemicals. We can't have our alchemists running around endangering our last bastion of hope, now can we?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end