/************************************************************************************

Copyright   :   Copyright 2017 Oculus VR, LLC. All Rights reserved.

Licensed under the Oculus VR Rift SDK License Version 3.4.1 (the "License");
you may not use the Oculus VR Rift SDK except in compliance with the License,
which is provided at the time of installation or download, or which
otherwise accompanies this software in either electronic or hard copy form.

You may obtain a copy of the License at

https://developer.oculus.com/licenses/sdk-3.4.1

Unless required by applicable law or agreed to in writing, the Oculus VR SDK
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

************************************************************************************/

using System;
using UnityEngine;

/// <summary>
/// An object that can be grabbed and thrown by OVRGrabber.
/// </summary>
public class OVRGrabbable : MonoBehaviour
{
    [SerializeField]
    protected bool m_allowOffhandGrab = true;
    [SerializeField]
    protected bool m_snapPosition = false;
    [SerializeField]
    protected bool m_snapOrientation = false;
    [SerializeField]
    protected Transform m_snapOffset;
    [SerializeField]
    protected Collider[] m_grabPoints = null;

    protected bool m_grabbedKinematic = false;
    protected Collider m_grabbedCollider = null;
    protected OVRGrabber m_grabbedBy = null;

    /// <summary>
    /// If true, the object can currently be grabbed.
    /// </summary>
    public bool allowOffhandGrab
    {
        get { return m_allowOffhandGrab; }
    }

    /// <summary>
    /// If true, the object is currently grabbed.
    /// </summary>
    public bool isGrabbed
    {
        get { return m_grabbedBy != null; }
    }

    // start of my adds
    public GameObject box;
    public GameObject nonDominantHand;
    public GameObject dominantHand;
    private Vector3 oldPosition;
    private Vector3 offsetTwoHands;

    private Vector3 boxPosition;

    private Vector3 boxLocalScale;
    private float oldDistance;

    // 1: dominant just used
    // 2: non dominant just used
    private int status = -1;

    // non dominant part
    // public GameObject dominantHand;
    private Vector3 oldOffset;
    private Vector3 oldOffsetLeft;
    private Vector3 oldOffsetRight;

    // HOC
    
    public void Update()
    {
        if (isGrabbed)
        {
            if (transform.tag.Equals("dominant"))
            {
                Debug.Log("dominant");
                offsetTwoHands = transform.position - nonDominantHand.transform.position;

                // box.transform.localScale += transform.position / 50 - oldPosition / 50;

                float distance = Vector3.Distance(transform.position, nonDominantHand.transform.position);

                Vector3 temp;
                temp.x = boxLocalScale.x * distance / oldDistance;
                temp.y = boxLocalScale.y * distance / oldDistance;
                temp.z = boxLocalScale.z * distance / oldDistance;

                box.transform.localScale = temp;

                // Debug.Log("dominant position: " + distance + " old distance: " + oldDistance);

                status = 1;
            }

            if (transform.tag.Equals("nonDominant"))
            {
                Debug.Log("nonDominant");
                box.transform.position = transform.position - oldOffset;


                status = 2;
            }
        }

        if (!isGrabbed)
        {
            if (status == 1)
            {
                // Debug.Log(transform.tag);
                // transform.transform.position = 
                boxPosition = box.transform.position;

                Vector3 temp;
                temp.x = box.transform.position.x - box.transform.localScale.x / 2;
                temp.y = box.transform.position.y - box.transform.localScale.y / 2;
                temp.z = box.transform.position.z - box.transform.localScale.z / 2;

                // Debug.Log(temp.x);
                nonDominantHand.transform.position = temp;

                // reset right hand position
                Vector3 temp2;
                temp2.x = box.transform.position.x + box.transform.localScale.x / 2;
                temp2.y = box.transform.position.y + box.transform.localScale.y / 2;
                temp2.z = box.transform.position.z + box.transform.localScale.z / 2;

                // disable this line would change HOC -> HIM
                dominantHand.transform.position = temp2;
            }

            if(status == 2)
            {
                Vector3 temp;
                temp.x = box.transform.position.x + box.transform.localScale.x / 2;
                temp.y = box.transform.position.y + box.transform.localScale.y / 2;
                temp.z = box.transform.position.z + box.transform.localScale.z / 2;

                // disable this line would change HOC -> HIM
                dominantHand.transform.position = temp;
            }

            status = -1;
            
        }
    }
    



    // HIM
    /*
    public void Update()
    {
        if (isGrabbed)
        {
            if (transform.tag.Equals("dominant"))
            {
                Debug.Log("dominant");
                offsetTwoHands = transform.position - nonDominantHand.transform.position;

                // box.transform.localScale += transform.position / 50 - oldPosition / 50;

                float distance = Vector3.Distance(transform.position, nonDominantHand.transform.position);

                Vector3 temp;
                temp.x = boxLocalScale.x * distance / oldDistance;
                temp.y = boxLocalScale.y * distance / oldDistance;
                temp.z = boxLocalScale.z * distance / oldDistance;

                box.transform.localScale = temp;

                // Debug.Log("dominant position: " + distance + " old distance: " + oldDistance);

                status = 1;
            }

            if (transform.tag.Equals("nonDominant"))
            {
                Debug.Log("nonDominant");
                box.transform.position = transform.position - oldOffset;


                status = 2;
            }
        }

        if (!isGrabbed)
        {
            if (status == 1)
            {
                // Debug.Log(transform.tag);
                // transform.transform.position = 
                boxPosition = box.transform.position;

                Vector3 temp;
                // temp.x = box.transform.position.x - box.transform.localScale.x / 2;
                temp.x = box.transform.position.x;
                temp.y = box.transform.position.y - box.transform.localScale.y / 2;
                temp.z = box.transform.position.z;
                // temp.z = box.transform.position.z - box.transform.localScale.z / 2;

                // Debug.Log(temp.x);
                nonDominantHand.transform.position = temp;
            }

            if (status == 2)
            {
                Vector3 temp;
                temp.x = box.transform.position.x + box.transform.localScale.x / 2;
                temp.y = box.transform.position.y + box.transform.localScale.y / 2;
                temp.z = box.transform.position.z + box.transform.localScale.z / 2;

                // disable this line would change HOC -> HIM
                // dominantHand.transform.position = temp;
            }

            status = -1;

        }
    }
    */

    // TC
    /*
    public void Update()
    {
        if (isGrabbed)
        {
            if (transform.tag.Equals("dominant"))
            {
                Debug.Log("dominant");

                float distance = Vector3.Distance(nonDominantHand.transform.position, dominantHand.transform.position);

                Vector3 temp;
                temp.x = boxLocalScale.x * distance / oldDistance;
                temp.y = boxLocalScale.y * distance / oldDistance;
                temp.z = boxLocalScale.z * distance / oldDistance;

                box.transform.localScale = temp;

                // Debug.Log("dominant position: " + distance + " old distance: " + oldDistance);

                box.transform.position = transform.position - oldOffsetRight;

                // Debug.Log("dominant position: " + distance + " old distance: " + oldDistance);

                status = 1;
            }

            if (transform.tag.Equals("nonDominant"))
            {
                Debug.Log("nonDominant");

                float distance = Vector3.Distance(nonDominantHand.transform.position, dominantHand.transform.position);

                Vector3 temp;
                temp.x = boxLocalScale.x * distance / oldDistance;
                temp.y = boxLocalScale.y * distance / oldDistance;
                temp.z = boxLocalScale.z * distance / oldDistance;

                box.transform.localScale = temp;

                // Debug.Log("dominant position: " + distance + " old distance: " + oldDistance);

                box.transform.position = transform.position - oldOffsetLeft;


                status = 2;
            }


        }

        if (!isGrabbed)
        {

            if (status == 1)
            {
                boxPosition = box.transform.position;

                Vector3 temp;
                temp.x = box.transform.position.x - box.transform.localScale.x / 2;
                temp.y = box.transform.position.y - box.transform.localScale.y / 2;
                temp.z = box.transform.position.z - box.transform.localScale.z / 2;

                nonDominantHand.transform.position = temp;


                Vector3 temp2;
                temp2.x = box.transform.position.x + box.transform.localScale.x / 2;
                temp2.y = box.transform.position.y + box.transform.localScale.y / 2;
                temp2.z = box.transform.position.z + box.transform.localScale.z / 2;

                dominantHand.transform.position = temp2;
            }

            if (status == 2)
            {
                boxPosition = box.transform.position;


                Vector3 temp;
                temp.x = box.transform.position.x + box.transform.localScale.x / 2;
                temp.y = box.transform.position.y + box.transform.localScale.y / 2;
                temp.z = box.transform.position.z + box.transform.localScale.z / 2;

                dominantHand.transform.position = temp;

                Vector3 temp2;
                temp2.x = box.transform.position.x - box.transform.localScale.x / 2;
                temp2.y = box.transform.position.y - box.transform.localScale.y / 2;
                temp2.z = box.transform.position.z - box.transform.localScale.z / 2;

                nonDominantHand.transform.position = temp2;
            }

            status = -1;

        }

    }
    */


    // end of my adds

    /// <summary>
    /// If true, the object's position will snap to match snapOffset when grabbed.
    /// </summary>
    public bool snapPosition
    {
        get { return m_snapPosition; }
    }

    /// <summary>
    /// If true, the object's orientation will snap to match snapOffset when grabbed.
    /// </summary>
    public bool snapOrientation
    {
        get { return m_snapOrientation; }
    }

    /// <summary>
    /// An offset relative to the OVRGrabber where this object can snap when grabbed.
    /// </summary>
    public Transform snapOffset
    {
        get { return m_snapOffset; }
    }

    /// <summary>
    /// Returns the OVRGrabber currently grabbing this object.
    /// </summary>
    public OVRGrabber grabbedBy
    {
        get { return m_grabbedBy; }
    }

    /// <summary>
    /// The transform at which this object was grabbed.
    /// </summary>
    public Transform grabbedTransform
    {
        get { return m_grabbedCollider.transform; }
    }

    /// <summary>
    /// The Rigidbody of the collider that was used to grab this object.
    /// </summary>
    public Rigidbody grabbedRigidbody
    {
        get { return m_grabbedCollider.attachedRigidbody; }
    }

    /// <summary>
    /// The contact point(s) where the object was grabbed.
    /// </summary>
    public Collider[] grabPoints
    {
        get { return m_grabPoints; }
    }

    /// <summary>
    /// Notifies the object that it has been grabbed.
    /// </summary>
    virtual public void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        m_grabbedBy = hand;
        m_grabbedCollider = grabPoint;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    /// <summary>
    /// Notifies the object that it has been released.
    /// </summary>
    virtual public void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        // rb.isKinematic = m_grabbedKinematic;
        rb.velocity = linearVelocity;
        rb.angularVelocity = angularVelocity;
        m_grabbedBy = null;
        m_grabbedCollider = null;
    }

    void Awake()
    {
        if (m_grabPoints.Length == 0)
        {
            // Get the collider from the grabbable
            Collider collider = this.GetComponent<Collider>();
            if (collider == null)
            {
                throw new ArgumentException("Grabbables cannot have zero grab points and no collider -- please add a grab point or collider.");
            }

            // Create a default grab point
            m_grabPoints = new Collider[1] { collider };
        }
    }

    protected virtual void Start()
    {
        m_grabbedKinematic = GetComponent<Rigidbody>().isKinematic;

        // my adds
        // dominantHand.GetComponent<Renderer>().material.color = new Color(1.0f, 0.5f, 1.0f, 1.0f);
        // nonDominantHand.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        // HOC
        
        oldDistance = Vector3.Distance(transform.position, nonDominantHand.transform.position);
        boxLocalScale = box.transform.localScale;
        oldOffset = nonDominantHand.transform.position - box.transform.position;
        


        // HIM
        /*
        oldDistance = Vector3.Distance(transform.position, nonDominantHand.transform.position);
        boxLocalScale = box.transform.localScale;
        oldOffset = nonDominantHand.transform.position - box.transform.position;
        oldOffset.x = 0;
        oldOffset.z = 0;
        nonDominantHand.transform.position = box.transform.position + oldOffset;
        */

        // TC

        /*
        oldDistance = Vector3.Distance(dominantHand.transform.position, nonDominantHand.transform.position);
        boxLocalScale = box.transform.localScale;
        oldOffsetLeft = nonDominantHand.transform.position - box.transform.position;
        oldOffsetRight = dominantHand.transform.position - box.transform.position;
        */
    }

    void OnDestroy()
    {
        if (m_grabbedBy != null)
        {
            // Notify the hand to release destroyed grabbables
            m_grabbedBy.ForceRelease(this);
        }
    }
}
