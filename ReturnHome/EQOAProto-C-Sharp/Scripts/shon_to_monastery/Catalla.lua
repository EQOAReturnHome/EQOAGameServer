function event_say()
diagOptions = {}
    npcDialogue = "I wasn't accepted by the Qeynos monastery so I came here. I still haven't been accepted but at least the monks allow me to train and live with them."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end