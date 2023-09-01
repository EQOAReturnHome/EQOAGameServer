function event_say()
diagOptions = {}
    npcDialogue = "It brings me great sadness to hear reports of the Teir'Dal plundering treasures of our elven legacy from Takish'Hiz and delivering them to Neriak. We  have lost so much of our ancestry...we must find a way to stop this horrific depravity."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end