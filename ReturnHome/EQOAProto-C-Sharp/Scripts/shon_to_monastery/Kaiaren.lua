function event_say()
diagOptions = {}
    npcDialogue = "Shon-To would not listen to reason. He believed we should only focus on making our bodies as honed and deadly as possible. This is no path for a monk to walk. There is so much more to the world then combat."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end