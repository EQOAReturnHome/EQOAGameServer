function event_say()
diagOptions = {}
    npcDialogue = "The more I observe the moon at night, the more I am certain someone lives there. How can there be this whole other world in the sky, yet no being roam the surface?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end