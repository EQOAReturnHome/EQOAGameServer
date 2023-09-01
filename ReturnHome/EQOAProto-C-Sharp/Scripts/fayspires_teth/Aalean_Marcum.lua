function event_say()
diagOptions = {}
    npcDialogue = "I study many ancient runes found throughout the world. Many runes tell interesting stories. Others cast spells of protection or healing. Some have strange effects that are widely unknown. A few even tell events that have not come to pass. I'll tell you a secret, playerName. One rune even foretold about the destruction of this fine city. In this telling, all the elves were long expelled from Fayspires and Tethelin, and a frigid darkness had taken over. My heart cannot bare the truth of it, and I have not yet been able to tell the others yet. Please, keep this between us for now."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end