function event_say()
diagOptions = {}
    npcDialogue = "We guardians are trained to avoid combat whenever possible. We attempt to disable the enemy before bloodshed, but are perfectly capable of delivering a fatal blow when it is necessary."
SendDialogue(mySession, npcDialogue, diagOptions)
end