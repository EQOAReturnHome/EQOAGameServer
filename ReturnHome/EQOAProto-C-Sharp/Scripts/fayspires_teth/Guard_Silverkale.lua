function event_say()
diagOptions = {}
    npcDialogue = "Follow this road southeast, and stay on it as it curves around the lake to reach Tethelin. Or, if you wish travel east from here to reach Kara Village, and beyond to find the gnome city of Klick A'non."
SendDialogue(mySession, npcDialogue, diagOptions)
end