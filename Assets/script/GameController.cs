using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //10*10��int�^�Q�����z����`
    private int[,] squares = new int[5, 5];

    //WHITE=1,BLACK=-1�Œ�`
    private const int EMPTY = 0;
    private const int WHITE = 1;
    private const int BLACK = -1;

    //���݂̃v���C���[(�����v���C���[�͔�)
    private int currentPlayer = WHITE;

    //�J�������
    private Camera camera_object;
    private RaycastHit hit;

    //prefabs
    public GameObject whiteStone;
    public GameObject blackStone;

    // Start is called before the first frame update
    void Start()
    {
        //�J���������擾
        camera_object = GameObject.Find("Main Camera").GetComponent<Camera>();

        //�z���������
        InitializeArray();

        //�f�o�b�O�p���\�b�h
        DebugArray();

    }

    // Update is called once per frame
    void Update()
    {
        int x = 0;
        int z = 0;

        //��΂������Ă��邩�ǂ����m�F����
        if (CheckStone(WHITE) || CheckStone(BLACK))
        {
            return;
        }

        //���̃^�[���̂Ƃ�(Player)
        if (currentPlayer == WHITE)
        {

            //�}�E�X���N���b�N���ꂽ�Ƃ�
            if (Input.GetMouseButtonDown(0))
            {
                //�}�E�X�̃|�W�V�������擾����Ray�ɑ��
                Ray ray = camera_object.ScreenPointToRay(Input.mousePosition);

                //�}�E�X�̃|�W�V��������Ray�𓊂��ĉ����ɓ���������hit�ɓ����
                if (Physics.Raycast(ray, out hit))
                {
                    //x,z�̒l���擾
                    x = (int)hit.collider.gameObject.transform.position.x;
                    z = (int)hit.collider.gameObject.transform.position.z;

                    Debug.Log(x);
                    Debug.Log(z);

                    //�}�X����̂Ƃ�
                    if (squares[z, x] == EMPTY)
                    {
                        
                            //Squares�̒l���X�V
                            squares[z, x] = WHITE;

                            //Stone���o��
                            GameObject stone = Instantiate(whiteStone);
                            stone.transform.position = hit.collider.gameObject.transform.position;

                            //Player�����
                            currentPlayer = BLACK;
                        
                    }
                }
            }
        }

        //���̃^�[���̂Ƃ�(NPC)
        else if (currentPlayer == BLACK)
        {
            while (true)
            {
                x = (int)(Random.Range(1.0f, 5.0f));
                z = (int)(Random.Range(1.0f, 5.0f));

                if (squares[z, x] == EMPTY)
                {
                    Debug.Log(x);
                    Debug.Log(z);
                    break;
                }
            }
            
            //Squares�̒l���X�V
            squares[z, x] = BLACK;

            //Stone���o��
            GameObject stone = Instantiate(blackStone);

            Vector3 pos = stone.transform.position;
            pos.x = x;
            pos.z = z;

            stone.transform.position = pos;

            //Player�����
            currentPlayer = WHITE;
            
        }
    }

    //�z���������������
    private void InitializeArray()
    {
        //for���𗘗p���Ĕz��ɃA�N�Z�X����
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                //�z�����i�l���O�j�ɂ���
                squares[i, j] = EMPTY;
            }
        }
    }

    //�z������m�F����i�f�o�b�O�p�j
    private void DebugArray()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Debug.Log("(i,j) = (" + i + "," + j + ") = " + squares[i, j]);
            }
        }
    }

    private bool CheckStone(int color)
    {
        //��΂̐����J�E���g����
        int count = 0;

        //������
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                //squares�̒l����̂Ƃ�
                if (squares[i, j] == EMPTY || squares[i, j] != color)
                {
                    //count������������
                    count = 0;
                }
                else
                {
                    //count��squares�̒l���i�[����
                    count++;
                }

                //count�̒l��5�ɂȂ����Ƃ�
                if (count == 5)
                {
                    //���̂Ƃ�
                    if (color == WHITE)
                    {
                        Debug.Log("���̏����I�I�I");
                    }
                    //���̂Ƃ�
                    else
                    {
                        Debug.Log("���̏����I�I�I");
                    }

                    return true;
                }
            }
        }

        //count�̒l��������
        count = 0;

        //�c����
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                //squares�̒l����̂Ƃ�
                if (squares[j, i] == EMPTY || squares[j, i] != color)
                {
                    //count������������
                    count = 0;
                }
                else
                {
                    //count��squares�̒l���i�[����
                    count++;
                }

                //count�̒l��5�ɂȂ����Ƃ�
                if (count == 5)
                {
                    //���̂Ƃ�
                    if (color == WHITE)
                    {
                        Debug.Log("���̏����I�I�I");
                    }
                    //���̂Ƃ�
                    else
                    {
                        Debug.Log("���̏����I�I�I");
                    }

                    return true;
                }
            }
        }

        //count�̒l��������
        count = 0;

        //�΂�(�E�オ��)
        for (int i = 0; i < 5; i++)
        {
            //��ړ��p
            int up = 0;

            for (int j = i; j < 5; j++)
            {
                //squares�̒l����̂Ƃ�
                if (squares[j, up] == EMPTY || squares[j, up] != color)
                {
                    //count������������
                    count = 0;
                }
                else
                {
                    count++;
                }

                //count�̒l��5�ɂȂ����Ƃ�
                if (count == 5)
                {
                    //���̂Ƃ�
                    if (color == WHITE)
                    {
                        Debug.Log("���̏����I�I�I");
                    }
                    //���̂Ƃ�
                    else
                    {
                        Debug.Log("���̏����I�I�I");
                    }

                    return true;
                }

                up++;
            }
        }

        //count�̒l��������
        count = 0;

        //�΂�(�E������)
        for (int i = 0; i < 5; i++)
        {
            //���ړ��p
            int down = 4;

            for (int j = i; j < 5; j++)
            {
                //squares�̒l����̂Ƃ�
                if (squares[j, down] == EMPTY || squares[j, down] != color)
                {
                    //count������������
                    count = 0;
                }
                else
                {
                    count++;
                }

                //count�̒l��5�ɂȂ����Ƃ�
                if (count == 5)
                {
                    //���̂Ƃ�
                    if (color == WHITE)
                    {
                        Debug.Log("���̏����I�I�I");
                    }
                    //���̂Ƃ�
                    else
                    {
                        Debug.Log("���̏����I�I�I");
                    }

                    return true;
                }

                down--;
            }
        }

        //�܂����肪���Ă��Ȃ��Ƃ�
        return false;
    }
}


