function event_say()
diagOptions = {}
    npcDialogue = "Do you see those hills just beyond the road to the southeast? I watch them like a hawk for roaming orcs. I have been known to take one down from this very tower. I have excellent eyesight, but I confess that it wouldn't be possible without this enchanted bow."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end